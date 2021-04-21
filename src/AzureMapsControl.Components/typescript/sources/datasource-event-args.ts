import { EventArgs } from '../events/event';
import { Shape } from '../geometries/geometry';

export interface DataSourceEventArgs extends EventArgs {
    id: string;
    shapes?: Shape[];
}