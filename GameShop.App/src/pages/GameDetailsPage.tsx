import axios from "axios";
import { useEffect, useState } from "react";
import { Game } from "../models/Game";
import { useParams } from "react-router-dom";
import { AddToCartButton } from "../components/games/AddToCartButton";
import { ReviewList } from "../components/reviews/ReviewList";
import { SubmitReview } from "../components/games/SubmitReview";

export const GameDetailsPage = () => {
  const [game, setGame] = useState<Game | null>(null);
  const { id } = useParams();

  useEffect(() => {
    axios
      .get(`${import.meta.env.VITE_GAME_DETAILS_URL}${id}`)
      .then((response) => setGame(response.data))
      .catch((error) => console.log(error));
  }, []);

  return (
    <div className="container">
      <div className="container d-flex mt-5">
        <div
          className="me-5 rounded"
          style={{
            border: "1px solid black",
            height: "400px",
            width: "800px",
          }}
        >
          <iframe
            width={"100%"}
            height={"100%"}
            src=""
            title="YouTube video player"
            allow="accelerometer; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
            allowFullScreen
          ></iframe>
        </div>
        <div
          className="rounded"
          style={{
            height: "400px",
            width: "600px",
          }}
        >
          <div>
            <h3>{game?.title}</h3>
          </div>
          <div>
            <a href="#">{game?.publisher}</a>
          </div>
          <div>
            <h3>${game?.price}</h3>
          </div>
          <small>Platform: {game?.platform}</small>
          <AddToCartButton id={id!} />
          <SubmitReview gameId={id} />
        </div>
      </div>
      <div className="mt-5">
        <h1>Reviews</h1>
        <ReviewList id={id!} />
      </div>
    </div>
  );
};
