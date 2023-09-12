import { useEffect, useState } from "react";
import { Game } from "../../models/Game";
import axios from "axios";
import { Loader } from "../common/Loader";
import { SingleGame } from "./SingleGame";
import { PagingList } from "./PagingList";

export const GameList = () => {
  const [games, setGames] = useState<Game[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [page, setPage] = useState(1);
  const [pageTotal, setPageTotal] = useState(0);

  const handleGetGames = (page: number) => {
    axios
      .get(`${import.meta.env.VITE_ALL_GAMES_URL}${page}`)
      .then((response) => {
        setGames(response.data.payload);
        setPage(response.data.startingIndex);
        setPageTotal(response.data.pageTotal);
        setIsLoading(false);
      })
      .catch((error) => console.log(error));
  };

  useEffect(() => {
    handleGetGames(page);
  }, []);

  if (isLoading) {
    return <Loader />;
  } else {
    return (
      <>
        <div className="d-flex justify-content-center text-center row row-cols-3 g-3 p-5">
          {games.map((game) => (
            <SingleGame key={game.id} game={game} />
          ))}
        </div>
        <PagingList
          pageTotal={pageTotal}
          currentPage={page}
          handleGetItems={handleGetGames}
        />
      </>
    );
  }
};
