import { EventArgs } from './event';

export interface EventHelper<T extends EventArgs | any> {
    invokeMethodAsync(method: string, payload: T): void;
}