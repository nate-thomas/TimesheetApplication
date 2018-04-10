import { WorkPackage } from '../workpackages/workPackages'

export class Project {
    projectNumber: string;
    statusName: string;
    description: string;
    budget: number;
    workPackages: string[];
    projectManager: string;

    constructor() { }
}