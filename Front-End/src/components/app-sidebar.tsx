"use client"

import * as React from "react"
import {
  BookOpen,
  Box,
  Command,
  Container,
  Layers,
  LifeBuoy,
  Network,
  Send,
  Settings2,
  Activity,
} from "lucide-react"

import { NavMain } from "@/components/nav-main"
import { NavSecondary } from "@/components/nav-secondary"
import { NavUser } from "@/components/nav-user"
import {
  Sidebar,
  SidebarContent,
  SidebarFooter,
  SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from "@/components/ui/sidebar"

const data = {
  user: {
    name: "Naamloos",
    email: "naamloos@example.org",
    avatar: "https://nudes.zip/server-icon.png",
  },
  navMain: [
    {
      title: "Dashboard",
      url: "/dashboard",
      icon: Activity,
      isActive: true,
      items: [
        {
          title: "Overview",
          url: "/dashboard/overview",
        },
        {
          title: "Analytics",
          url: "/dashboard/analytics",
        },
      ],
    },
    {
      title: "Containers",
      url: "/containers",
      icon: Box,
      items: [
        {
          title: "Running",
          url: "/containers/running",
        },
        {
          title: "Stopped",
          url: "/containers/stopped",
        },
        {
          title: "All Containers",
          url: "/containers/all",
        },
      ],
    },
    {
      title: "Images",
      url: "/images",
      icon: Layers,
      items: [
        {
          title: "Local Images",
          url: "/images/local",
        },
        {
          title: "Pull Image",
          url: "/images/pull",
        },
        {
          title: "Build History",
          url: "/images/history",
        },
      ],
    },
    {
      title: "Networks",
      url: "/networks",
      icon: Network,
      items: [
        {
          title: "All Networks",
          url: "/networks/all",
        },
        {
          title: "Create Network",
          url: "/networks/create",
        },
      ],
    },
    {
      title: "Volumes",
      url: "/volumes",
      icon: Container,
      items: [
        {
          title: "All Volumes",
          url: "/volumes/all",
        },
        {
          title: "Create Volume",
          url: "/volumes/create",
        },
      ],
    },
    {
      title: "Settings",
      url: "/settings",
      icon: Settings2,
      items: [
        {
          title: "General",
          url: "/settings/general",
        },
        {
          title: "Docker Host",
          url: "/settings/host",
        },
        {
          title: "Preferences",
          url: "/settings/preferences",
        },
      ],
    },
  ],
  navSecondary: [
    {
      title: "Documentation",
      url: "/docs",
      icon: BookOpen,
    },
    {
      title: "Support",
      url: "/support",
      icon: LifeBuoy,
    },
    {
      title: "Feedback",
      url: "/feedback",
      icon: Send,
    },
  ],
}

export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  return (
    <Sidebar variant="inset" {...props}>
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg" asChild>
              <a href="/">
                <div className="bg-sidebar-primary text-sidebar-primary-foreground flex aspect-square size-8 items-center justify-center rounded-lg">
                  <Command className="size-4" />
                </div>
                <div className="grid flex-1 text-left text-sm leading-tight">
                  <span className="truncate font-medium">Ducker Dashboard</span>
                  {/* <span className="truncate text-xs">Management</span> */}
                </div>
              </a>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
        <NavSecondary items={data.navSecondary} className="mt-auto" />
      </SidebarContent>
      <SidebarFooter>
        <NavUser user={data.user} />
      </SidebarFooter>
    </Sidebar>
  )
}
