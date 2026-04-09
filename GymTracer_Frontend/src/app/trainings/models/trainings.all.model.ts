export type AllTrainingResponse = {
  id: number;
  name: string;
  image: string;
  startTime: string;
  endTime: string;
  maxParticipant: number;
  trainer: {
    id: number;
    name: string;
  };
}

export type AllTrainingFilter = {
  start?: Date | string;
  end?: Date | string;
  trainerName?: string;
  trainerId?: number;
  keyword?: string;
}

export type AllTrainingFilterLocal = {
  start: Date | string;
  end: Date | string;
  trainerName: string;
  trainerId?: number;
  keyword: string;
}