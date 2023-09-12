interface Props {
  id: string;
}

export const AddToCartButton = ({ id }: Props) => {
  const handleAddCartItem = () => {
    let old = localStorage.getItem("ids");
    if (old?.includes(id)) {
      return;
    }
    old = old + ` ${id}`;
    localStorage.setItem("ids", `${old}`);
  };

  return (
    <>
      <div>
        <button
          className="btn btn-danger w-25 mt-3"
          onClick={() => handleAddCartItem()}
        >
          Add To Cart
        </button>
      </div>
    </>
  );
};
