"use client"

import * as React from "react"
import {
  Box,
  Container,
  Layers,
  Network,
  Settings2,
  Activity,
  Github, // TODO replace - deprecated icon
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

import logo from "@/assets/logo.png"
import { Link, usePage } from "@inertiajs/react"
import type PageProps from "@/@types/page-props"

const data = {
  navMain: [
    {
      title: "Dashboard",
      url: "/",
      icon: Activity,
      isActive: true,
      items: [
        {
          title: "Overview",
          url: "/",
        },
        {
          title: "Analytics",
          url: "/",
        },
      ],
    },
    {
      title: "Containers",
      url: "/",
      icon: Box,
      items: [
        {
          title: "Running",
          url: "/",
        },
        {
          title: "Stopped",
          url: "/",
        },
        {
          title: "All Containers",
          url: "/",
        },
      ],
    },
    {
      title: "Images",
      url: "/",
      icon: Layers,
      items: [
        {
          title: "Local Images",
          url: "/",
        },
        {
          title: "Pull Image",
          url: "/",
        },
        {
          title: "Build History",
          url: "/",
        },
      ],
    },
    {
      title: "Networks",
      url: "/",
      icon: Network,
      items: [
        {
          title: "All Networks",
          url: "/",
        },
        {
          title: "Create Network",
          url: "/",
        },
      ],
    },
    {
      title: "Volumes",
      url: "/",
      icon: Container,
      items: [
        {
          title: "All Volumes",
          url: "/",
        },
        {
          title: "Create Volume",
          url: "/",
        },
      ],
    },
    {
      title: "Settings",
      url: "/",
      icon: Settings2,
      items: [
        {
          title: "General",
          url: "/",
        },
        {
          title: "Docker Host",
          url: "/",
        },
        {
          title: "Preferences",
          url: "/",
        },
      ],
    },
  ],
  navSecondary: [
    {
      title: "GitHub",
      url: "https://github.com/Naamloos/ducker-dashboard",
      icon: Github,
    }
  ],
}

export function AppSidebar({ ...props }: React.ComponentProps<typeof Sidebar>) {
  const { user } = usePage<PageProps>().props;
  return (
    <Sidebar variant="inset" {...props}>
      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton size="lg" asChild>
              <Link href="/">
                <img src={logo} alt="Ducker Logo" className="size-10"/>
                <div className="grid flex-1 text-left text-sm leading-tight">
                  <span className="truncate font-medium">Ducker Dashboard</span>
                  {/* <span className="truncate text-xs">Management</span> */}
                </div>
              </Link>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>
      <SidebarContent>
        <NavMain items={data.navMain} />
        <NavSecondary items={data.navSecondary} className="mt-auto" />
      </SidebarContent>
      <SidebarFooter>
        <NavUser user={{
          name: user?.username? (user.username[0].toUpperCase() + user.username.slice(1)) : "User",
          avatar: logo
        }} />
      </SidebarFooter>
    </Sidebar>
  )
}
