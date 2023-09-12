import { GameOrder } from "./GameOrder";

export interface Order {
  id?: number;
  amount?: number;
  numberOfGames?: number;
  gameIds?: number[];
  createdBy?: String;
  createTime?: Date;
  lastEditTime?: Date;
  gameOrders?: GameOrder[];
}
