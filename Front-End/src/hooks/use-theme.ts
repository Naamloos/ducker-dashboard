import { useEffect, useState, useCallback, useRef } from "react"
import type React from "react"

type Theme = "light" | "dark" | "system"

function getStoredTheme(): Theme {
    const stored = localStorage.getItem("theme")
    if (stored === "light" || stored === "dark") return stored
    return "system"
}

export function useTheme() {
    const [theme, setThemeState] = useState<Theme>(getStoredTheme())
    const didMountRef = useRef(false)
    const clickPointRef = useRef<{ x: number; y: number } | null>(null)

    // Apply theme to document
    useEffect(() => {
        const root = document.documentElement

        const applyResolvedTheme = (mode: "light" | "dark") => {
            // Tailwind dark variant
            root.classList.toggle("dark", mode === "dark")
            // Optional: also keep data-theme in sync for CSS variables/token systems
            root.setAttribute("data-theme", mode)
        }

        const prefersReducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches
        type VTDoc = Document & { startViewTransition?: (update: () => void) => ViewTransition }
        type ViewTransition = { ready: Promise<void>; finished: Promise<void>; updateCallbackDone: Promise<void> }
        const doc = document as VTDoc
        const canViewTransition = typeof doc.startViewTransition === "function" && !prefersReducedMotion && didMountRef.current

    const runWithTransition = (fn: () => void, targetMode?: "light" | "dark") => {
            // If we can't or shouldn't animate, just run.
            if (!canViewTransition) {
                fn()
                clickPointRef.current = null
                return
            }

            const point = clickPointRef.current
            const transition = doc.startViewTransition!(fn)
            if (!point) {
                // Simple cross-fade: rely on default view transition styling
                transition.finished.then(() => {
                    clickPointRef.current = null
                })
                return
            }

            const root = document.documentElement
            root.style.setProperty("--theme-transition-x", point.x + "px")
            root.style.setProperty("--theme-transition-y", point.y + "px")
            // Add helper class plus direction so CSS can target ::view-transition-new(root)
            const goingDark = targetMode === "dark"
            if (targetMode) {
                root.classList.add("theme-radial-transition", goingDark ? "theme-to-dark" : "theme-to-light")
            } else {
                root.classList.add("theme-radial-transition")
            }

            transition.finished.then(() => {
                root.classList.remove("theme-radial-transition", "theme-to-dark", "theme-to-light")
                root.style.removeProperty("--theme-transition-x")
                root.style.removeProperty("--theme-transition-y")
                clickPointRef.current = null
            })
        }

        if (theme === "system") {
            localStorage.removeItem("theme")
            const media = window.matchMedia("(prefers-color-scheme: dark)")

            const apply = () => {
                const mode: "light" | "dark" = media.matches ? "dark" : "light"
                runWithTransition(() => applyResolvedTheme(mode), mode)
            }

            // Apply immediately and subscribe to changes
            apply()
            media.addEventListener("change", apply)
            return () => media.removeEventListener("change", apply)
        } else {
            localStorage.setItem("theme", theme)
            runWithTransition(() => applyResolvedTheme(theme), theme)
        }
    }, [theme])

    // Mark mounted so subsequent theme changes animate; avoid animating initial paint
    useEffect(() => {
        didMountRef.current = true
    }, [])

    // Listen for storage changes (multi-tab sync)
    useEffect(() => {
        const onStorage = (e: StorageEvent) => {
            if (e.key === "theme") {
                setThemeState(getStoredTheme())
            }
        }
        window.addEventListener("storage", onStorage)
        return () => window.removeEventListener("storage", onStorage)
    }, [])

    const setTheme = useCallback((newTheme: Theme, opts?: { event?: React.MouseEvent | MouseEvent; x?: number; y?: number }) => {
        if (opts?.event) {
            clickPointRef.current = { x: opts.event.clientX, y: opts.event.clientY }
        } else if (typeof opts?.x === "number" && typeof opts?.y === "number") {
            clickPointRef.current = { x: opts.x, y: opts.y }
        } else {
            clickPointRef.current = null
        }
        setThemeState(newTheme)
    }, [])

    return { theme, setTheme }
}