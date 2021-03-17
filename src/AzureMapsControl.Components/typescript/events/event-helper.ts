import { EventArgs } from './event';

export interface EventHelper<T extends EventArgs> {
    invokeMethodAsync(method: string, payload: T): void;
}