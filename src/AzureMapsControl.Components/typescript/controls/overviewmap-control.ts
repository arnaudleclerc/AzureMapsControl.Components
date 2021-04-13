import * as overviewmap from 'azure-maps-control-overviewmap';
import { Core } from '../core/core';

export class OverviewMapControl {
    public static setOptions(controlId: string, options: overviewmap.OverviewMapOptions): void {
        (Core.getMap().controls.getControls().find(ctrl => (ctrl as any).amc && (ctrl as any).amc.id === controlId) as overviewmap.control.OverviewMap).setOptions(options);
    }
}