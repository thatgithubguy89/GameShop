import axios, { AxiosResponse } from "axios";
import { useState } from "react";
import { GameOrder } from "../../models/GameOrder";
import { useNavigate } from "react-router-dom";

interface Props {
  gameId?: String;
}

export const SubmitReview = ({ gameId }: Props) => {
  const [gameOrder, setGameOrder] = useState<GameOrder>();
  const [orderNumber, setOrderNumber] = useState("");
  const navigate = useNavigate();

  const handleGetGameOrder = async (e: {
    preventDefault: () => void;
  }): Promise<AxiosResponse | any> => {
    e.preventDefault();

    await axios
      .get(
        `${import.meta.env.VITE_SINGLE_GAMEORDER_URL}${gameId}/${orderNumber}`
      )
      .then((response) => setGameOrder(response.data))
      .catch((error) => console.log(error));

    if (gameOrder) {
      if (!gameOrder?.hasBeenReviewed) {
        navigate(
          `/createreview/${gameId}/${gameOrder?.id}/${gameOrder?.createdBy}`
        );
      }
    }
  };

  return (
    <>
      <form className="d-flex mt-3">
        <input
          type="text"
          className="form-control w-25 me-3"
          placeholder="Order Number"
          onChange={(e) => setOrderNumber(e.target.value)}
          required
        />
        <button className="btn btn-primary" onClick={handleGetGameOrder}>
          Submit Review
        </button>
      </form>
    </>
  );
};
