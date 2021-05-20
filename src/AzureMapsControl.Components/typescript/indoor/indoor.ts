import * as indoor from 'azure-maps-indoor';
import { Core } from '../core/core';
import { IndoorManagerOptions } from './options';

export class Indoor {

    private static readonly _indoorManagers = new Map<string, indoor.indoor.IndoorManager>();

    public static createIndoorManager(id: string, options: IndoorManagerOptions): void {
        let levelControl: indoor.control.LevelControl;
        if (options && options.levelControl) {
            levelControl = new indoor.control.LevelControl(options.levelControl.options);
        }

        const indoorManager = new indoor.indoor.IndoorManager(Core.getMap(), {
            levelControl,
            statesetId: options.statesetId,
            theme: options.theme,
            tilesetId: options.tilesetId
        });

        this._indoorManagers.set(id, indoorManager);
    }

    public static dispose(id: string): void {
        const indoorManager = this._getIndoorManager(id);
        indoorManager.dispose();
        this._indoorManagers.delete(id);
    }

    public static initialize(id: string): void {
        const indoorManager = this._getIndoorManager(id);
        indoorManager.initialize();
    }

    private static _getIndoorManager(id: string): indoor.indoor.IndoorManager {
        if (this._indoorManagers.has(id)) {
            return this._indoorManagers.get(id);
        }
        return null;
    }
}