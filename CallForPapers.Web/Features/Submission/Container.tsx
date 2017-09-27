import React from 'react'
import { connect } from 'react-redux'
import { actionCreators, FormState, Submission } from './store'
import { createTypedFormComponents } from '~/forms'

type Props = FormState & typeof actionCreators

const { TextCell } = createTypedFormComponents<Submission>()

class Container extends React.Component<Props> {
  render () {
    const commonProps = {
      form: this.props.model,
      onFieldChange: this.props.onFieldChange
    }
    return (
      <div style={{ display: 'flex', flexDirection: 'column' }}>
        <h1>Submission</h1>
        <TextCell {...commonProps} field="firstName" placeholder="First name" />
        <TextCell {...commonProps} field="lastName" placeholder="Last name" />
        <TextCell {...commonProps} field="title" placeholder="Title" />
        <TextCell {...commonProps} field="abstract" placeholder="Abstract" />
        <input type="button" value="Submit" onClick={this.props.submitForm} />
      </div>
    )
  }
}

export default connect((state) => state.form, actionCreators)(Container)
