import { useNavigate } from "react-router-dom";
import { Game } from "../../models/Game";

interface Props {
  game: Game;
}

export const SingleGame = ({ game }: Props) => {
  const navigate = useNavigate();

  const handleNavigate = () => {
    navigate(`/gamedetails/${game.id}`);
  };

  return (
    <>
      <div className="col">
        <div
          className="card"
          style={{ cursor: "pointer" }}
          onClick={() => handleNavigate()}
        >
          <img
            src={`${import.meta.env.VITE_BASE_URL}${game.imagePath}`}
            className="card-img-top"
            alt="Hollywood Sign on The Hill"
            style={{ maxWidth: "100%" }}
          />
          <div className="card-body">
            <h5 className="card-title">{game.title}</h5>
          </div>
        </div>
      </div>
    </>
  );
};
