import * as indoor from 'azure-maps-indoor';
import { Core } from '../core/core';
import { IndoorManagerOptions } from './options';

export class Indoor {

    private static readonly _indoorManagers = new Map<string, indoor.indoor.IndoorManager>();

    public static createIndoorManager(id: string, options: IndoorManagerOptions): void {
        let levelControl: indoor.control.LevelControl;
        if (options.levelControl) {
            levelControl = new indoor.control.LevelControl(options.levelControl.options);
        }

        if (!options.theme) {
            options.theme = 'auto';
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

    public static getCurrentFacility(id: string): { facilityId: string, levelOrdinal: number } {
        const indoorManager = this._getIndoorManager(id);
        const currentFacility = indoorManager.getCurrentFacility();
        return { facilityId: currentFacility[0], levelOrdinal: currentFacility[1] };
    }

    public static getOptions(id: string): IndoorManagerOptions {
        const indoorManager = this._getIndoorManager(id);
        const options = indoorManager.getOptions();
        return {
            statesetId: options.statesetId,
            theme: options.theme,
            tilesetId: options.tilesetId
        };
    }

    public static getStyleDefinition(id: string): indoor.indoor.IStyleDefinition {
        const indoorManager = this._getIndoorManager(id);
        return indoorManager.getStyleDefinition();
    }

    public static setDynamicStyling(id: string, enabled: boolean): void {
        const indoorManager = this._getIndoorManager(id);
        indoorManager.setDynamicStyling(enabled);
    }

    public static setFacility(id: string, facilityId: string, levelOrdinal: number): void {
        const indoorManager = this._getIndoorManager(id);
        indoorManager.setFacility(facilityId, levelOrdinal);
    }

    public static setOptions(id: string, options: IndoorManagerOptions): void {
        const indoorManager = this._getIndoorManager(id);
        let levelControl: indoor.control.LevelControl;

        if (options.levelControl) {
            levelControl = new indoor.control.LevelControl(options.levelControl.options);
        }

        if (!options.theme) {
            options.theme = 'auto';
        }

        indoorManager.setOptions({
            levelControl,
            statesetId: options.statesetId,
            theme: options.theme,
            tilesetId: options.tilesetId
        });
    }

    private static _getIndoorManager(id: string): indoor.indoor.IndoorManager {
        if (this._indoorManagers.has(id)) {
            return this._indoorManagers.get(id);
        }
        return null;
    }
}