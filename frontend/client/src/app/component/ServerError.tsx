import { useLocation } from "react-router-dom";


export default function ServerError() {
  const { state } = useLocation();
  const error = state?.error;

  return (
    <div>
      <h1>Server Error</h1>
      <p>{error}</p>
    </div>
  );
}
