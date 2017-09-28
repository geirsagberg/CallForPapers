import { renderApp } from './reactUtils'
import { configureStore } from './reduxUtils'
import { ReducersMapObject, combineReducers } from 'redux'

export interface PageOptions {
  elementId?: string
  Container
  reducers?: ReducersMapObject
  initialState?
}

export default function initPage ({ elementId = 'app', Container, reducers, initialState }: PageOptions) {
  const mount = document.getElementById(elementId)
  const store = configureStore(reducers, initialState)
  const render = (Container) => renderApp(Container, store, mount)
  render(Container)
  const updateStore = () => store.replaceReducer(combineReducers(reducers))
  return {
    render,
    updateStore
  }
}
