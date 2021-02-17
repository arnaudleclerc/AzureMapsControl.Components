/**
 * Helper class for merging namespaces.
 */
export class Namespace {
    public static merge(namespace: string, base: object) {
        let context = window || global;
        const parts = namespace.split(".");

        for (const part of parts) {
            if (context[part]) {
                context = context[part];
            } else {
                return base;
            }
        }

        return { ...context, ...base };
    }
}
