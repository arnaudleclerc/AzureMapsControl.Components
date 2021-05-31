import { EventArgs } from '../events/event';

export interface IndoorEventArgs
    extends EventArgs {
    facilityId: string;
    levelNumber: number;
    prevFacilityId: string;
    prevLevelNumber: number;
}