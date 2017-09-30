import { createStore, applyMiddleware, compose, GenericStoreEnhancer, ReducersMapObject, combineReducers } from 'redux'
import thunk from 'redux-thunk'

export function configureStore (reducers: ReducersMapObject, initialState?) {
  // Build middleware. These are functions that can process the actions before they reach the store.
  const windowIfDefined = typeof window === 'undefined' ? null : window as any
  // If devTools is installed, connect to it
  const devToolsExtension: () => GenericStoreEnhancer = windowIfDefined && windowIfDefined.devToolsExtension

  const middlewareEnhancer = applyMiddleware(thunk)
  const devToolsEnhancer = devToolsExtension ? devToolsExtension() : (f) => f

  const createStoreWithMiddleware = compose(middlewareEnhancer, devToolsEnhancer)(createStore)

  // Combine all reducers and instantiate the app-wide store instance
  const combinedReducer = combineReducers(reducers)
  return createStoreWithMiddleware(combinedReducer, initialState)
}