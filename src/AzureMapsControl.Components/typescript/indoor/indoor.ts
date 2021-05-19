import * as indoor from 'azure-maps-indoor';
import { Core } from '../core/core';
import { IndoorManagerOptions } from './options';

export class Indoor {

    private static readonly _indoorManagers = new Map<string, indoor.indoor.IndoorManager>();
    private static readonly _levelControls = new Map<string, indoor.control.LevelControl>();

    public static createIndoorManager(id: string, options: IndoorManagerOptions): void {
        let levelControl: indoor.control.LevelControl;
        if (options) {
            if (options.levelControl) {
                levelControl = this._getLevelControl(options.levelControl.id);
                if (!levelControl) {
                    levelControl = new indoor.control.LevelControl(options.levelControl.options);
                    this._levelControls.set(options.levelControl.id, levelControl);
                }
            }
        }

        const indoorManager = new indoor.indoor.IndoorManager(Core.getMap(), {
            levelControl,
            statesetId: options.statesetId,
            theme: options.theme,
            tilesetId: options.tilesetId
        });

        this._indoorManagers.set(id, indoorManager);
    }

    private static _getIndoorManager(id: string): indoor.indoor.IndoorManager {
        if (this._indoorManagers.has(id)) {
            return this._indoorManagers.get(id);
        }
        return null;
    }

    private static _getLevelControl(id: string): indoor.control.LevelControl {
        if (this._levelControls.has(id)) {
            return this._levelControls.get(id);
        }
        return null;
    }

}