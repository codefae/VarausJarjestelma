export interface TimeSlotDto {
    startTime: string; // TimeSpan doesn't exist in TypeScript, so using string (e.g., "HH:mm:ss")
    endTime: string;
}