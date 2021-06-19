/* eslint-disable @typescript-eslint/member-ordering */
/* eslint-disable quotes */
/* eslint-disable @typescript-eslint/prefer-readonly */
/* eslint-disable @typescript-eslint/naming-convention */
/* eslint-disable @typescript-eslint/prefer-namespace-keyword */
// eslint-disable-next-line max-classes-per-file
import * as azmaps from "azure-maps-control";

declare namespace atlas {
    export module control {
        /**
         * A control for changing the level of the indoor map.
         */
        export class LevelControl implements azmaps.Control {
            private readonly DOUBLE_CLICK_TIMEOUT;
            private readonly MIN_SLIDER_LEVELS_COUNT;
            private readonly MIN_ZOOM_LEVEL;

            private doubleClickTimeout;
            private hasDoubleClicked;
            private indoorManager;
            private levelControlContainer;
            private map;
            private options;
            private prevLevelsSet;
            private sliderDiv;
            private uiLevelOrdinal;
            /**
             * Constructs a LevelControl.
             * @param options The options for the control.
             */
            constructor(options?: LevelControlOptions);
            /**
             * Initialization method for the control which is called when added to the map.
             * @return An HTMLElement to be placed on the map for the control.
             */
            onAdd(): HTMLElement;
            /**
             * Method that is called when the control is removed from the map. Should perform any necessary cleanup for the
             * control.
             */
            onRemove(): void;

            private _close;
            private _open;
            private constructButtons;
            private constructSlider;
            private generateLevelControl;
            private getLevelFeatures;
            private onFacilityChange;
            private onMapClick;
            private onMapDoubleCLick;
            private onMapLoad;
            private onMapZoom;
            private setInUse;
            private updateLevelControlVisibility;
        }

        /**
         * The options for a LevelControlOptions object.
         */
        export class LevelControlOptions {
            /**
             * The position of the control.
             * Default `atlas.ControlPosition.TopRight`
             * @default atlas.ControlPosition.TopRight
             */
            public position?: azmaps.ControlPosition;
            /**
             * The style of the control.
             * Default `ControlStyle.light`.
             * @default ControlStyle.light
             */
            public style?: azmaps.ControlStyle;
        }
    }

    export module indoor {
        /**
         * Event object returned by map when the facility changes.
         */
        export interface IFacilityChangeEvent extends azmaps.MapEvent {
            /**
             * The facility that the map is changing to.
             */
            facilityId: string;
            /**
             * The level ordinal that the map is changing to.
             */
            levelNumber: number;
            /**
             * The facility that the map is changing out of.
             */
            prevFacilityId: string;
            /**
             * The level number that the map is changing out of.
             */
            prevLevelNumber: number;
        }
        /**
         * Event object returned by the maps when the level changes.
         */
        export interface ILevelChangeEvent extends azmaps.MapEvent {
            /**
             * The facility that the map is changing to.
             */
            facilityId: string;
            /**
             * The level number that the map is changing to.
             */
            levelNumber: number;
            /**
             * The facility that the map is changing out of.
             */
            prevFacilityId: string;
            /**
             * The level number that the map is changing out of.
             */
            prevLevelNumber: number;
        }

        /**
         * The events supported by the `Indoor Manager`.
         */
        export interface IIndoorManagerEvents {
            /**
             * Event object returned by map when the level number changes.
             */
            facilitychanged: IFacilityChangeEvent;
            /**
             * Event object returned by map when the level number changes.
             */
            levelchanged: ILevelChangeEvent;
        }

        /**
         * A representation of a style definition.
         */
        export interface IStyleDefinition {
            /**
             * The base path of the style.
             */
            domain: string;
            /**
             * Style version.
             */
            version: string;
            /**
             * A description of a style with configuration paths.
             */
            styles: IStyleDefinitionStyle[];
        }

        /**
         * A representation of a style definition's style object.
         */
        export interface IStyleDefinitionStyle {
            /**
             * Style name.
             */
            name: string;
            /**
             * Style theme. `light` or `dark`.
             */
            theme: "dark" | "light";
            /**
             * Style's sprite path.
             */
            spritePath: string;
            /**
             * List of layer groups, defining layer names and their layer style paths.
             */
            layerGroups: IStyleDefinitionLayerGroup[];
        }

        /**
         * A representation of layer name and its style path.
         */
        export interface IStyleDefinitionLayerGroup {
            /**
             * Layer name.
             */
            name: string;
            /**
             * Layer style path.
             */
            layerPath: string;
        }

        /**
         * An IndoorManager handles loading the indoor maps styles.
         */
        export class IndoorManager {
            private readonly currentMapping;
            private readonly facilityMapping;
            private readonly filterList;
            private readonly indoorLayers;
            private readonly masterFilter;
            private readonly selectedFacility;

            private facilityOverlapHandler;
            private requestManager;
            private indoorLayerIds;
            private indoorSourceIds;
            private isDebug;
            private isDynamicStylingEnabled;
            private map;
            private oldConsoleError;
            private prevIndoorSourceCamera;
            private queueForIndoorStyleUpdate;

            private currentMapTheme?;
            private options?;
            private stateTileDelegate?;
            /**
             * Constructs an IndoorManager.
             * @param map The map to overlay indoor styles on.
             * @param options The options for the manager.
             */
            constructor(map: azmaps.Map, options?: IndoorManagerOptions);
            /**
             * Adds or updates the indoor styles to the map.
             */
            initialize(): void;
            /**
             * Disposes the IndoorManager.
             * When disposed, all resources used by the IndoorManager are released.
             */
            dispose(): void;
            /**
             * Retrieves the currently selected facility.
             * [ currentFacilityId, currentLevelOrdinal ]
             * Note: The returned type may change to an object.
             */
            getCurrentFacility(): [string, number];
            /**
             * Gets the options used by the IndoorManager.
             */
            getOptions(): IndoorManagerOptions;
            /**
             * Retrieves the style definition that is used in this indoor style manager.
             */
            getStyleDefinition(): IStyleDefinition;
            /**
             * Turn on or off Dynamic Styling.
             * Turn on Dynamic Styling will involve calls to Get State Tile.
             * Turn off Dynamic Styling will stop calling Get State Tile.
             * Requires Initiate Indoor Manager with options that have statesetId value set.
             * @param enable true to enable Dynamic Styling; false to disable Dynamic Styling.
             */
            setDynamicStyling(enable: boolean): void;
            /**
             * Sets the current facility and its ordinal.
             * If an empty string is provided for the facilityId,
             * it will reset any facility selection and remove the level picker.
             * @param facilityId The facility Id.
             * @param levelOrdinal The level ordinal number.
             */
            setFacility(facilityId: string, levelOrdinal: number): void;
            /**
             * Sets the options used by the IndoorManager.
             * @param options The indoor manager options.
             */
            setOptions(options: IndoorManagerOptions): void;
            private buildComponent;
            private clearIndoorStyles;
            private debugPrint;
            private focusFacilityLevel;
            private generateFilterList;
            private getIndoorSpritePath;
            private getMapTheme;
            private handleIndoorSource;
            private handleSourceData;
            private handleStyledata;
            private processTilesetThemeOptions;
            private updateIndoorSprites;
            private updateIndoorStyles;
            private updateMasterFilter;
            public _getCurrentMapping;
            public _getFacilityMapping;
        }

        /**
         * Options for the drawing manager.
         */
        export class IndoorManagerOptions {
            /**
             * The geography of the Creator resource.
             * @default "United States";
             */
            public geography?: string;

            /**
             * A level picker to display as a control for the indoor manager.
             */
            public levelControl?: atlas.control.LevelControl;

            /**
             * The state set id to pass with requests.
             * default: `undefined`
             * @default undefined
             */
            public statesetId?: string;

            /**
             * The theme for indoor layer styles.
             * Default `auto`.
             * @default "auto"
             */
            public theme?: "auto" | "dark" | "light";

            /**
             * The tileset id to pass with requests.
             * Default `undefined`.
             * @default undefined
             */
            public tilesetId?: string;
        }
    }
}

declare module "azure-maps-control" {
    export interface EventManager {
        /**
         * Adds a facility change event to the `IndoorManager`.
         * @param eventType The event name.
         * @param target The `IndoorManager` to add the event for.
         * @param callback The event handler callback.
         */
        add(eventType: "facilitychanged", target: atlas.indoor.IndoorManager, callback: (e: atlas.indoor.IFacilityChangeEvent) => void): void;
        /**
         * Adds a level change event to the `IndoorManager`.
         * @param eventType The event name.
         * @param target The `IndoorManager` to add the event for.
         * @param callback The event handler callback.
         */
        add(eventType: "levelchanged", target: atlas.indoor.IndoorManager, callback: (e: atlas.indoor.ILevelChangeEvent) => void): void;

        /**
         * Removes an event listener from the `IndoorManager`
         * @param eventType The event name.
         * @param target The `IndoorManager` to remove the event for.
         * @param callback The event handler callback.
         */
        remove(eventType: string, target: atlas.indoor.IndoorManager, callback: (e?: any) => void): void;
    }
}

export = atlas;
