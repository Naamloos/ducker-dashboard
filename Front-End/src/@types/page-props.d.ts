import type User from "./dto/user.dto";

type PageProps<T = unknown> = {
    user?: User;
} & T;

export default PageProps;