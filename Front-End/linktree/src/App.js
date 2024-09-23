import './App.css';
import { MyRoutes } from './myroutes/MyRouter';
import { BrowserRouter as Router } from 'react-router-dom';

function App() {
  return (
    <div className="App">
      <Router>
          <MyRoutes/>
      </Router> 

    </div>
  ); 
}

export default App;
