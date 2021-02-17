export interface Geometry {
    coordinates: any;
    bbox: {
        east: number;
        north: number;
        south: number;
        west: number;
    },
    id: string;
    type: 'LineString' | 'MultiLineString' | 'MultiPoint' | 'MultiPolygon' | 'Point' | 'Polygon';
}