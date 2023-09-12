import { Game } from "../../models/Game";
import { RemoveItemButton } from "./RemoveItemButton";

interface Props {
  game: Game;
}

export const SingleCartGame = ({ game }: Props) => {
  return (
    <>
      <div className="card mb-3">
        <div className="card-body">
          <div className="d-flex justify-content-between">
            <div className="d-flex flex-row align-items-center">
              <div>
                <img
                  src={`${import.meta.env.VITE_BASE_URL}${game.imagePath}`}
                  className="img-fluid rounded-3"
                  alt="Shopping item"
                  style={{ width: "65px" }}
                />
              </div>
              <div className="ms-3">
                <h5>{game.title}</h5>
              </div>
            </div>
            <div className="d-flex flex-row align-items-center">
              <div style={{ width: "80px" }}>
                <h5 className="mb-0">${game.price}</h5>
              </div>
              <RemoveItemButton id={game.id} />
            </div>
          </div>
        </div>
      </div>
    </>
  );
};
