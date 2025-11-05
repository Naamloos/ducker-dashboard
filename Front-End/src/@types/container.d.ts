export interface Container {
    id: string;
    name: string;
    image: string;
    state: string;
    status: string;
    created: string;
    labels: Record<string, string>;
    imageId: string;
    command: string;
}