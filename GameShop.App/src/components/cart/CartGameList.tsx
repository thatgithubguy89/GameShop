import { Game } from "../../models/Game";
import { SingleCartGame } from "./SingleCartGame";

interface Props {
  games: Game[];
}

export const CartGameList = ({ games }: Props) => {
  return (
    <>
      <div className="col-lg-7">
        {games.map((game) => (
          <SingleCartGame key={game.id} game={game} />
        ))}
      </div>
    </>
  );
};
