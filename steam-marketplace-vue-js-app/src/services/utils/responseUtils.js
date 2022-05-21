export const responseGet = async (client, url, config) => {
  return (await client.get(url, config)).data
}

export const responsePost = async (client, url, requestBody) => {
  return (await client.post(url, requestBody)).data
}