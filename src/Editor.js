import {useEffect, useState} from "react";
import {Mention} from "primereact/mention";

export const Editor = ({rawText, setRawText})=>{
    const [intelSuggestions, setIntelSuggestions] = useState([]);
    const delay = 400;
    useEffect(() => {
        const handler = setTimeout(() => {
            TriggerIntellisense(); // Your debounced action
        }, delay);

        return () => {
            clearTimeout(handler); // Cleanup timeout on new keystroke
        };
    }, [rawText, delay]);

    function TriggerIntellisense(){
        if(rawText === "@dev"){
            setIntelSuggestions(["@developer", "@development", "devenv"])
        }
    }

    const itemTemplate = (suggestion) => {

        return (
            <div className="flex align-items-center">
                <span className="flex flex-column ml-2">
                    {suggestion}
                    <small style={{ fontSize: '.75rem', color: 'var(--text-color-secondary)' }}>{suggestion}</small>
                </span>
            </div>
        );
    }

    return (
        /*<Mention
            className={"w-full h-screen text-lg suggestion-textarea"} pt={{input:"w-full"}} trigger={["@"]}
            value={rawText} onChange={(e)=>setRawText(e.target.value)}
                suggestions={intelSuggestions} itemTemplate={itemTemplate} onSearch={TriggerIntellisense}
                placeholder="Start Typing Fitnesse Code"   />*/

         <textarea className={"w-full h-screen text-lg suggestion-textarea"}
                   value={rawText} onChange={(e)=>setRawText(e.target.value)}
         />
    )
}