import AuthenticatedLayout from "@/layouts/authenticated_layout";
import type { Container } from "@/@types/container";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";

export default function Index({containers}: {containers: Container[]}) {
    console.log(containers);
  const formatDate = (timestamp: number) => {
    return new Date(timestamp * 1000).toLocaleString();
  };

  const getStateColor = (state: string) => {
    switch (state?.toLowerCase()) {
      case "running":
        return "text-green-400 font-semibold";
      case "exited":
        return "text-red-400 font-semibold";
      case "paused":
        return "text-yellow-400 font-semibold";
      default:
        return "text-gray-400";
    }
  };

  return (
    <AuthenticatedLayout>
      <div className="space-y-4">
        <h1 className="text-2xl font-bold">Ducker Dashboard (Development Version)</h1>
        
        <div className="rounded-md border">
          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Name</TableHead>
                <TableHead>Image</TableHead>
                <TableHead>State</TableHead>
                <TableHead>Status</TableHead>
                <TableHead>Created</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {containers && containers.length > 0 ? (
                containers.map((container) => (
                  <TableRow key={container.id}>
                    <TableCell className="font-medium">{container.name}</TableCell>
                    <TableCell>{container.image}</TableCell>
                    <TableCell className={getStateColor(container.state)}>
                      {container.state}
                    </TableCell>
                    <TableCell>{container.status}</TableCell>
                    <TableCell>{formatDate(container.created)}</TableCell>
                  </TableRow>
                ))
              ) : (
                <TableRow>
                  <TableCell colSpan={5} className="text-center text-muted-foreground">
                    No containers found
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </div>
      </div>
    </AuthenticatedLayout>
  );
}