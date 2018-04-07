import { WorkPackage } from './workPackages'

export class Project {
    projectNumber: string;
    statusName: string;
    description: string;
    budget: number;
    workPackages: WorkPackage[] = new Array();

    constructor() { }
}