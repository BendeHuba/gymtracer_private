export interface TrainingModel {
    id:             number;
    name:           string;
    description:    string;
    image:          string;
    startTime:      Date;
    endTime:        Date;
    maxParticipant: number;
    trainer:        Trainer;
}

export interface Trainer {
    id:   number;
    name: string;
}

