import { useEffect, useState } from "react";
import { Game } from "../../models/Game";
import axios from "axios";
import { Loader } from "../common/Loader";
import { PagingList } from "../games/PagingList";

export const CommentList = () => {
  const [comments, setComments] = useState<Game[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [pageTotal, setPageTotal] = useState(0);

  const handleGetComments = (page: number) => {
    axios
      .get(`${import.meta.env.VITE_ALL_GAMES_URL}${page}`)
      .then((response) => {
        setComments(response.data.payload);
        setPage(response.data.startingIndex);
        setPageTotal(response.data.pageTotal);
        setIsLoading(false);
      })
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    handleGetComments(page);
  }, []);

  if (isLoading) {
    return <Loader />;
  } else {
    return (
      <>
        <div className="d-flex justify-content-center text-center row row-cols-3 g-3 p-5">
          {comments.map((comment) => (
            <SingleComment key={comment.id} game={comment} />
          ))}
        </div>
        <PagingList
          pageTotal={pageTotal}
          currentPage={page}
          handleGetGames={handleGetComments}
        />
      </>
    );
  }
};
