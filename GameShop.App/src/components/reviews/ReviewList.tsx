import { useEffect, useState } from "react";
import axios from "axios";
import { Loader } from "../common/Loader";
import { PagingList } from "../games/PagingList";
import { SingleReview } from "./SingleReview";
import { Review } from "../../models/Review";

interface Props {
  id: string;
}

export const ReviewList = ({ id }: Props) => {
  const [reviews, setReviews] = useState<Review[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [pageTotal, setPageTotal] = useState(0);

  const handleGetReviews = (page: number) => {
    const gameReviewsRequest = {
      gameId: id,
      page: page,
    };

    axios
      .post(import.meta.env.VITE_GAME_REVIEWS_URL, gameReviewsRequest)
      .then((response) => {
        setReviews(response.data.payload);
        setPage(response.data.startingIndex);
        setPageTotal(response.data.pageTotal);
        setIsLoading(false);
      })
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    handleGetReviews(page);
  }, []);

  if (isLoading) {
    return <Loader />;
  } else {
    return (
      <>
        <div className="row row-cols-3 g-3">
          {reviews.map((review) => (
            <SingleReview key={review.id} review={review} />
          ))}
        </div>
        <PagingList
          pageTotal={pageTotal}
          currentPage={page}
          handleGetItems={handleGetReviews}
        />
      </>
    );
  }
};
