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
      url: "/dashboard",
      icon: Activity,
      items: [
        {
          title: "Overview",
          url: "/dashboard",
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
          title: "All Containers",
          url: "/containers",
        },
        {
          title: "Add New Container",
          url: "/containers/new",
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
          url: "/images",
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
          url: "/networks",
        },
        {
          title: "Create Network",
          url: "/networks/new",
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
          url: "/volumes",
        },
        {
          title: "Create Volume",
          url: "/volumes/new",
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
          url: "/settings",
        },
        {
          title: "Docker Host",
          url: "/settings/docker",
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
  const page = usePage<PageProps>();
  const { user } = page.props;
  // Inertia exposes the current URL as page.url; fall back to window for safety.
  // @inertiajs/react adds `url` on the page object in runtime, but it's not in our PageProps type.
  const currentUrl = (page as unknown as { url?: string }).url || (typeof window !== 'undefined' ? window.location.pathname + window.location.search : '/');
  const currentPath = currentUrl.split(/[?#]/)[0];

  // Compute active state for main & sub items.
  const computedNavMain = React.useMemo(() => {
    return data.navMain.map(item => {
      const subItems = item.items?.map(sub => ({
        ...sub,
        isActive: currentPath === sub.url
      })) || [];
      const hasActiveSub = subItems.some(s => s.isActive);
      const isParentActive = currentPath === item.url || currentPath.startsWith(item.url + '/') || hasActiveSub;
      return {
        ...item,
        isActive: isParentActive,
        items: subItems
      };
    });
  }, [currentPath]);
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
        <NavMain items={computedNavMain} />
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
