import { useEffect, useState } from "react";
import { CartForm } from "../components/cart/CartForm";
import { CartGameList } from "../components/cart/CartGameList";
import axios from "axios";
import { Game } from "../models/Game";
import { Loader } from "../components/common/Loader";

export const CartPage = () => {
  const [games, setGames] = useState<Game[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  const handleParseIds = () => {
    let idString = localStorage.getItem("ids");
    let idStringArray = idString?.split(" ");
    let idNumberArray: number[] = [];

    idStringArray?.forEach((id) => idNumberArray.push(Number(id)));

    return idNumberArray;
  };

  const handleGetTotal = (games: Game[]) => {
    let total: number | any = 0;

    for (let index = 0; index < games.length; index++) {
      total += games[index].price;
    }

    return total;
  };

  useEffect(() => {
    let ids = handleParseIds();

    axios
      .post(import.meta.env.VITE_CART_GAMES_URL, ids)
      .then((response) => {
        setGames(response.data);
        setIsLoading(false);
      })
      .catch((error) => console.log(error));
  }, []);

  if (isLoading) {
    <Loader />;
  } else {
    return (
      <>
        <div className="h-100 h-custom" style={{ backgroundColor: "#eee" }}>
          <div className="container py-5 h-100">
            <div className="row d-flex justify-content-center align-items-center h-100">
              <div className="col">
                <div className="card">
                  <div className="card-body p-4">
                    <div className="row">
                      <CartGameList games={games} />
                      <div className="col-lg-5">
                        <div className="card bg-primary text-white rounded-3">
                          <div className="card-body">
                            <div className="d-flex justify-content-between align-items-center mb-4"></div>
                            <CartForm
                              total={handleGetTotal(games)}
                              numberOfGames={games.length}
                              gameIds={handleParseIds()}
                            />
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </>
    );
  }
};
