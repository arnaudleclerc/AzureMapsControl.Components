import * as fullscreencontrol from 'azure-maps-control-fullscreen';

export class FullscreenControl {

    public static async isFullscreenSupported(): Promise<boolean> {
        return Promise.resolve(fullscreencontrol.control.FullscreenControl.isSupported());
    }

}