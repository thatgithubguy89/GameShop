export interface GameOrder {
  id: number;
  hasBeenReviewed?: boolean;
  gameId?: number;
  orderId?: number;
  createdBy?: String;
  createTime?: Date;
  lastEditTime?: Date;
}
