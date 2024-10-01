export interface Rooms {
  id: number;
  buildingId: number;
  name: string;
  roomTypeId: number;
  roomType: string; // или создайте отдельный интерфейс для RoomType, если у него есть дополнительные свойства
  capacity: number;
  floor: number;
  number: number;
}