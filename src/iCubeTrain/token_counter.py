from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
import tiktoken

app = FastAPI()

class TokenRequest(BaseModel):
    text: str

@app.post("/count_tokens")
async def count_tokens(request: TokenRequest):
    try:
        encoding = tiktoken.encoding_for_model("gpt-3.5-turbo")
        tokens = encoding.encode(request.text)
        return len(tokens)
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="localhost", port=8000)
