import { EventArgs } from '../events';

export interface EventHelper {
    invokeMethodAsync(method: string, payload: EventArgs): void;
}