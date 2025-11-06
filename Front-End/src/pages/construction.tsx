import AuthenticatedLayout from "@/layouts/authenticated_layout";

export default function Construction() {
  return (
    <AuthenticatedLayout>
      <div className="h-full w-full flex items-center justify-center">
        <div className="flex flex-col text-center gap-6">
          <span className="text-4xl font-bold">🚧</span>
          <h1 className="ml-4 text-3xl font-semibold">Under construction</h1>
          <p>
            This page is currently under construction. Use the sidebar to navigate to a different
            section.
          </p>
        </div>
      </div>
    </AuthenticatedLayout>
  );
}