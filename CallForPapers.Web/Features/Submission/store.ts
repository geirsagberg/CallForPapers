import { actionBuilderFactory } from '~/reduxUtils'

export type FormState = { form: Submission }

export interface AppState {
  formState: FormState
}

export interface Submission {
  firstName: string
  lastName: string
  title: string
  abstract: string
}

enum ActionTypes {
  OnFieldChange = 'ON_FIELD_CHANGE',
  ResetForm = 'RESET_FORM'
}

interface OnFieldChange {
  type: ActionTypes.OnFieldChange
  field: string
  newValue
}

interface ResetForm {
  type: ActionTypes.ResetForm
}

type Action = OnFieldChange | ResetForm

const actionBuilder = actionBuilderFactory<Action>()

const onFieldChange = actionBuilder<OnFieldChange>(ActionTypes.OnFieldChange)('field', 'newValue')
const resetForm = actionBuilder<ResetForm>(ActionTypes.ResetForm)()

const submitForm = () => (dispatch, getState: () => AppState) => {
  const form = getState().formState.form
  const { firstName, lastName, title, abstract } = form
  fetch('/api/Submission', {
    method: 'post',
    body: JSON.stringify({ firstName, lastName, title, abstract }),
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json'
    }
  }).then((response) => {
    if (response.ok) {
      dispatch(resetForm())
      alert('Paper submitted!')
    } else {
      response.json().then((json) => {
        alert(response.status + ' ' + response.statusText + ' ' + JSON.stringify(json))
      })
    }
  })
}

export const actionCreators = {
  onFieldChange,
  submitForm
}

const initialFormState: FormState = {
  form: {
    firstName: '',
    lastName: '',
    title: '',
    abstract: ''
  }
}

function formReducer (state = initialFormState, action: Action): FormState {
  switch (action.type) {
    case ActionTypes.OnFieldChange:
      return { ...state, form: { ...state.form, [action.field]: action.newValue } }
    case ActionTypes.ResetForm:
      return initialFormState
  }
  return state
}

export const reducers = {
  form: formReducer
}