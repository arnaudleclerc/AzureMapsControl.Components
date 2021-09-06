export interface Geometry {
    coordinates: any;
    bbox: number[],
    type: 'LineString' | 'MultiLineString' | 'MultiPoint' | 'MultiPolygon' | 'Point' | 'Polygon';
}

export interface Feature {
    id?: string;
    bbox: number[],
    geometry: Geometry,
    properties: { [key: string]: any }
}

export interface Shape {
    id: string;
    geometry: Geometry;
    properties: { [key: string]: any }
}