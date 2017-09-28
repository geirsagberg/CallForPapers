import initPage from '~/initPage'
import { reducers } from './store'
import Container from './Container'

const { render, updateStore } = initPage({ Container, reducers })

if (module && module.hot) {
  module.hot.accept('./Container', () => render(Container))
  module.hot.accept('./store', updateStore)
}
