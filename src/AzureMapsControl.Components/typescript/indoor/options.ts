import * as indoor from 'azure-maps-indoor';

export interface IndoorManagerOptions {
    levelControl?: {
        options: indoor.control.LevelControlOptions;
    };
    statesetId: string;
    theme: 'auto' | 'dark' | 'light';
    tilesetId: string;
}