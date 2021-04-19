import * as azmaps from 'azure-maps-control';
import { HtmlMarkerDefinition } from '../html-markers/html-marker-options';

export interface HtmlMarkerLayerInvokeHelper {
    invokeMethodAsync(method: string, id: string, position: azmaps.data.Position, properties: any): Promise<HtmlMarkerDefinition>;
}