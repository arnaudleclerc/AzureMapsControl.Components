import { Geometry } from './geometry';

export interface RoutePoint extends Geometry {
    timestamp: Date;
}