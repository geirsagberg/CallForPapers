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

/** Returns a function that builds action-builders for a family of action types, represented by the `ActionTypes` type argument */
export function actionBuilderFactory<TActions extends { type: string }> () {
  return function<T extends { type: TActions['type'] }> (s: T['type']): ActionBuilder<T> {
    return (...keys: (keyof T)[]) => {
      return (...values: any[]) => {
        const action = { type: s } as any
        for (let i = 0, l = keys.length; i < l; i++) {
          action[keys[i]] = values[i]
        }
        return action as T
      }
    }
  }
}

export interface ActionBuilder<TAction> {
  (): () => TAction
  <K1 extends keyof TAction>(k1: K1): (v1: TAction[K1]) => TAction
  <K1 extends keyof TAction, K2 extends keyof TAction>(k1: K1, k2: K2): (v1: TAction[K1], v2: TAction[K2]) => TAction
  <K1 extends keyof TAction, K2 extends keyof TAction, K3 extends keyof TAction>(k1: K1, k2: K2, k3: K3): (
    v1: TAction[K1],
    v2: TAction[K2],
    v3: TAction[K3]
  ) => TAction
}
