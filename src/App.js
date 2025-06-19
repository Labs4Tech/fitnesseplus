import './App.css';
import {useState} from "react";
import {Editor} from "./Editor";
import {FormatPipeTable} from "./helper";

const App = ()=> {
    const [rawText, setRawText] = useState('');


    return (
        <div className="App m-3">
            <div className={"header"}>
                <h1>Fitnesse Plus</h1>
            </div>

            <div className={"flex my-2"}>
                <button onClick={()=> setRawText(FormatPipeTable(rawText))}>Format</button>
            </div>
            <Editor rawText={rawText} setRawText={setRawText}/>
        </div>
  );
}

export default App;
