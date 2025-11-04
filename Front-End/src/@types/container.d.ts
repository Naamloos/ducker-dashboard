export interface Container {
    id: string;
    name: string;
    image: string;
    state: string;
    status: string;
    created: number;
    labels: Record<string, string>;
    imageId: string;
    command: string;
}