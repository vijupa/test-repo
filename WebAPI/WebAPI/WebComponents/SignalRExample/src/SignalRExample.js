
import React, { useEffect } from 'react'
import { HubConnectionBuilder } from '@microsoft/signalr';

export default function SignalRExample () {
  // Declare a new state variable, which we'll call "count"
  const [hubConnection, setHubConnection] = React.useState(0)
  const [count, setCount] = React.useState(0)
  const [buttonText, setButtonText] = React.useState('default button text')
  const message = `SignalR clicked ${count} times`

  useEffect(() => {

    const connection = new HubConnectionBuilder()
        .withUrl('#--API_URL--#/live')
        .withAutomaticReconnect()
        .build()
  
    setHubConnection(connection)
  }, [])

  useEffect(() => {
    const listen = async () => {
      console.log("hubConnection listen", hubConnection)
      if (hubConnection) {
        await hubConnection.start()
        hubConnection.on("clickCount", (a) => {
          console.log("count 1", a)
          setCount(a)
        });
      }
    }

    console.log("hubConnection", hubConnection)
    if (hubConnection) {      
      listen()
    }
  }, [hubConnection])

  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch('#--API_URL--#/api/buttontext')
      const json = await response.json()
      setButtonText(json.text)
    }

    fetchData()
  }, [setButtonText])

  return (
    <div>
      <p>{message}</p>
      <button type='button' onClick={() => setCount(count + 1)}>
        {buttonText}
      </button>
    </div>
  )
}

