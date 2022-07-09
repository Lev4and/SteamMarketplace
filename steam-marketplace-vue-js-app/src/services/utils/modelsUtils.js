export class BaseResponseModel {
  #result
  #status
  constructor(response) {
    this.#result = response?.result
    this.#status = new Status(response?.status)
  }

  get result() {
    return this.#result
  }

  get status() {
    return this.#status
  }
}

export class Status {
  #code
  #name
  #message
  constructor(status) {
    this.#code = status.code
    this.#name = status.name
    this.#message = status.message
  }

  get code() {
    return this.#code
  }

  get name() {
    return this.#name
  }

  get message() {
    return this.#message
  }

  get isSuccessful() {
    return this.#code === 200
  }
}

export class Login {
  password
  emailOrLogin
  constructor(login, password) {
    this.password = password
    this.emailOrLogin = login
  }
}
