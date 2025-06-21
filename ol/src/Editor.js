import {use, useEffect, useState} from "react";

export const Editor = ({rawText, setRawText})=>{
    const [intelSuggestions, setIntelSuggestions] = useState([]);
    const [suggestionPosition, setSuggestionPosition] = useState(0, 0);
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
        if(rawText.includes("@dev")){
            setIntelSuggestions(["@developer", "@development", "devenv"])
        }
        else setIntelSuggestions([]);
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
        <div className={"relative"}>
            {intelSuggestions.length > 0 &&
                <ul className={"absolute top-0"}>
                    {intelSuggestions.map((s, i) => (
                        <li
                            key={i}
                            // onClick={() => insertMention(s)}
                            style={{
                                padding: '8px',
                                cursor: 'pointer',
                                borderBottom: '1px solid #eee',
                            }}
                        >
                            @{s}
                        </li>
                    ))}
                </ul>
            }
             <textarea className={"w-full h-screen text-lg suggestion-textarea"}
                       value={rawText} onChange={(e)=>setRawText(e.target.value)}
             />

        </div>
    )
}