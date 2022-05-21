export const responseGet = async (client, url, config) => {
  return (await client.get(url, config).catch((error) => { return error.response.data })).data
}

export const responsePost = async (client, url, requestBody, config = null) => {
  config = config ?? { validateStatus: (status) => { return status < 500 } }
  return (await client.post(url, requestBody, config)).data
}